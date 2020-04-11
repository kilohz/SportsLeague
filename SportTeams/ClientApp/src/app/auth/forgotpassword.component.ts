import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../shared/services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-root',
    templateUrl: './ForgotPassword.component.html'
})

export class ForgotPasswordComponent implements OnInit {
    Submitted = false;

    ForgotPasswordForm = new FormGroup({
        Email: new FormControl('', [Validators.email, Validators.required])
    });

    constructor(public UserService: AuthService, private router: Router, private toastr: ToastrService) {
    }

    onSubmit() {
        this.ForgotPasswordForm.markAsTouched();
        this.Submitted = true;

        if (this.ForgotPasswordForm.valid) {
            this.UserService.ForgotPassword(this.ForgotPasswordForm.value["Email"])
                .toPromise().then(
                    (results: any) => {

                        if (results.result.succeeded) {
                            this.ForgotPasswordForm.reset();
                            this.toastr.success("An email has been sent to an admin to enable your account.", "Registration Successful!");
                            this.router.navigate(['/auth/login']);
                        }
                        else {
                            results.result.errors.forEach(element => {
                                switch (element.code) {
                                    case 'EmailDoesntExist':
                                        this.toastr.error("This email address doesnt exist", "Please register an account!")
                                        this.ForgotPasswordForm.reset();
                                        this.router.navigate(['auth/register']);
                                        break;
                                    default:
                                        this.toastr.error("If this error persists please contact an admin.", "An error occured the password reset!");
                                        break;
                                }
                            });
                        }
                    }
                );
        }

    }

    ngOnInit() {

    }
}
