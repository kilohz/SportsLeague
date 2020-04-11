import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../shared/services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-root',
    templateUrl: './Register.component.html'
})

export class RegisterComponent implements OnInit {

    RegisterForm = new FormGroup({
        FullName: new FormControl('', Validators.required),
        Email: new FormControl('', Validators.email),
        Password: new FormControl('', Validators.required),
        ConfirmPassword: new FormControl('', Validators.required),
        TermsAgreement: new FormControl('')
    });

    ErrorMessage = '';

    Submitted = false;

    constructor(public service: AuthService, private router: Router, private toastr: ToastrService) {

    }

    onSubmit(registerForm: FormGroup) {
        this.RegisterForm.markAsTouched();
        if (registerForm.valid && this.service.comparePasswords(registerForm)) {

            this.service.RegisterUser(registerForm)
                .toPromise().then(
                    (results: any) => {

                        if (results.result.succeeded) {
                            this.RegisterForm.reset();
                            this.router.navigate(['auth/login']).then(() => {
                                this.toastr.success("An email has been sent to an admin to enable your account.", "Registration Successful!");
                            });
                        }
                        else {
                            results.result.errors.forEach(element => {
                                switch (element.code) {
                                    case 'DuplicateUserName':
                                        this.toastr.error("Please try login with that email.", "UserName is already in use!")
                                        this.RegisterForm.reset();
                                        this.router.navigate(['auth/login']);
                                        break;
                                    case 'PasswordRequiresDigit':
                                        this.toastr.error("A password requires a capital letter and a number.", "Missing Number!");
                                        break;
                                    case 'PasswordRequiresUpper':
                                        this.toastr.error("A password requires a capital letter and a number.", "Missing Capital!");
                                        break;
                                    case 'PasswordRequiresLower':
                                        this.toastr.error("A password requires a capital letter and a number.", "Missing Letters!");
                                        break;
                                    case 'PasswordTooShort':
                                        this.toastr.error("A password is required to be at least 6 digits long.", "Password too short!");
                                        break;
                                    default:
                                        this.toastr.error("If this error persists please contact an admin.", "An error occured with your registration!");
                                        break;
                                }
                            });

                        }
                    }
                );
        } else if (!this.service.comparePasswords(registerForm)) {
            this.toastr.error("Please enter your passwords again.", "Passwords do not match!");
            this.Submitted = true;
        }
        else {
            this.Submitted = true;
        }
    }

    ngOnInit() {
        this.RegisterForm.reset();
    }
}

