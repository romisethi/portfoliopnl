import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.html',
  styleUrl: './signup.scss',
  standalone: true,
  imports: []
})
export class Signup {
  signupForm: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(private fb: FormBuilder, private userService: UserService, private router: Router) {
    this.signupForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.signupForm.invalid || this.signupForm.value.password !== this.signupForm.value.confirmPassword) return;
    this.loading = true;
    // TODO: Implement signup logic with userService
    setTimeout(() => {
      this.loading = false;
      this.router.navigate(['/user/login']);
    }, 1000);
  }
}
