import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-feedback-form',
  imports: [CommonModule, FormsModule],
  templateUrl: './feedback-form.html',
  styleUrl: './feedback-form.css',
})
export class FeedbackForm {
  departments = ['HR', 'Development', 'Design', 'QA'];

  allSkills = ['Angular', 'React', 'Node', 'Python'];

  feedback = {
    name : '',
    email : '',
    department : '',
    rating : '',
    comments : '',
    skills : [] as string[]
  };

  submitForm(form: NgForm){
    if(form.valid){
      console.log('Feedback Submitted', this.feedback);
      alert(JSON.stringify(this.feedback, null, 2));
      form.resetForm();
      this.feedback.skills = [];
    }
    else{
      alert('Please fill all required fields');
    }
  }

  updateSkills(skill : string, isChecked : boolean){
    if(isChecked){
      this.feedback.skills.push(skill);
    }
    else{
      const index = this.feedback.skills.indexOf(skill);
      if(index >= 0) this.feedback.skills.splice(index, 1);
    }
  }
}
