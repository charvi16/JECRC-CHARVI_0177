import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient-details',
  imports: [FormsModule],
  templateUrl: './patient-details.html',
  styleUrl: './patient-details.css',
})
export class PatientDetails {
  PatientName: string = '';
  DoctorSelection: string = '';
  AppointmentDate: string = '';  
  ConsultationType: string = '';
  Symptoms: string = '';

  doctors: string[] = [
    'Dr. Sharma',
    'Dr. Mehta',
    'Dr. Gupta'
  ];

  submitForm() {
    console.log({
      PatientName: this.PatientName,
      DoctorSelection: this.DoctorSelection,
      AppointmentDate: this.AppointmentDate,
      ConsultationType: this.ConsultationType,
      Symptoms: this.Symptoms
    });
  }
}
