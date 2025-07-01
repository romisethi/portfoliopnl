import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.html',
  styleUrl: './login.scss',
  standalone: true,
  imports: []
})
export class Login {
  loginForm: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(private fb: FormBuilder, private userService: UserService, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) return;
    this.loading = true;
    // TODO: Implement login logic with userService
    setTimeout(() => {
      this.loading = false;
      this.router.navigate(['/trades']);
    }, 1000);
  }
}
