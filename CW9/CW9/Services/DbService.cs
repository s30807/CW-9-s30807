using System.Data;
using CW9.Data;
using CW9.DTOs;
using CW9.Exceptions;
using CW9.Models;
using Microsoft.EntityFrameworkCore;

namespace CW9.Services;

public interface IDbService
{
    public Task<PrescriptionGetDto> AddPrescriptionAsync(PrescriptionCreateDto prescriptionDto);
    public Task<PatientGetDto> GetPatientDetailsByIdAsync(int id);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PrescriptionGetDto> AddPrescriptionAsync(PrescriptionCreateDto prescriptionDto)
    {
        //sprawdzenie dat
        if (prescriptionDto.DueDate < prescriptionDto.Date)
        {
            throw new DataException("Due date cannot be earlier than date itself");
        }
        
        //sprawdzenie ilosci lekow
        if (prescriptionDto.Medicaments.Count > 0)
        {
            throw new DataException("There can't be more than 10 medicaments");
        }
        
        //sprawdzenie czy istnieje pacjent
        if (prescriptionDto.Patient != null)
        {
            //patient o podanym id
            var patient = await data.Patients.FirstOrDefaultAsync(o => o.IdPatient == prescriptionDto.Patient.IdPatient);

            if (patient is null)
            {
                await AddPatientAsync(prescriptionDto.Patient);
            }
        }
        
        //sprawdzenie czy istnieje medicament
        if (prescriptionDto.Medicaments != null)
        {
            foreach (var medicamentDto in prescriptionDto.Medicaments)
            {
                var medicament = await data.Medicaments.FirstOrDefaultAsync(o => o.IdMedicament == medicamentDto.IdMedicament);
                if (medicament is null)
                {
                    throw new DataException("Medicament doesn't exist");
                }
            }
        }
        
        //
        var prescription = new Prescription
        {
            Date = prescriptionDto.Date,
            DueDate = prescriptionDto.DueDate,
            IdPatient = prescriptionDto.Patient.IdPatient,
            Patient = prescriptionDto.Patient,
            IdDoctor = prescriptionDto.Doctor.IdDoctor,
            Doctor = prescriptionDto.Doctor,
            Prescription_Medicament = new List<Prescription_Medicament>()
                .Select(e => new Prescription_Medicament
                {
                    Details = e.Details,
                    Dose = e.Dose,
                    Medicament = e.Medicament,
                    IdMedicament = e.IdMedicament,
                    Prescription = e.Prescription,
                    IdPrescription = e.IdPrescription,
                }).ToList(),
        };
        
        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();
        
        
        return new PrescriptionGetDto
        {
            Patient = prescription.Patient,
            Doctor = prescription.Doctor,
            Medicaments = prescription.Prescription_Medicament
                .Select(pm => new PrescriptionMedicamentGetDto
                {
                    IdMedicament = pm.IdMedicament,
                    Dose = pm.Dose,
                    Details = pm.Details,
                }).ToList(),
            Date = prescription.Date,
            DueDate = prescription.DueDate,
        };
    }
    
    //metoda dodajaca pacjenta
    private async Task<PatientGetDto> AddPatientAsync(Patient patient)
    {
        return new PatientGetDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate
        };
    }

    public async Task<PatientGetDto> GetPatientDetailsByIdAsync(int id)
    {
        var patient = await data.Patients.FirstOrDefaultAsync(p => p.IdPatient == id);
        if (patient is null)
            throw new NotFoundException($"Patient with id: {id} not found");
        
        return new PatientGetDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
        };
    }
    
}