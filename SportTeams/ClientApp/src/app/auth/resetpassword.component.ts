import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../shared/services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-root',
    templateUrl: './ResetPassword.component.html'
})

export class ResetPasswordComponent implements OnInit {
    Submitted = false;
    Email: string;
    Token: string;

    ResetForm = new FormGroup({
        Password: new FormControl('', [Validators.required]),
        PasswordConfirm: new FormControl('', [Validators.required])
    });

    constructor(public UserService: AuthService, private router: Router, private toastr: ToastrService, private route: ActivatedRoute) {
        this.route.queryParams.subscribe(params => {
            this.Email = params['Email'];
            this.Token = params['Token'];
            if (!this.Email || !this.Token) {
                this.router.navigate(['auth/login']).then(() => {
                    this.toastr.error("Bad link!");
                });
            }
        });
    }

    onSubmit(resetForm: FormGroup) {
        this.ResetForm.markAsTouched();
        this.Submitted = true;

        if (this.ResetForm.valid && this.ResetForm.value["Password"] == this.ResetForm.value["PasswordConfirm"]) {

            this.UserService.ResetPassword(resetForm.value["Password"], this.Email, this.Token)
                .toPromise().then(
                    (results: any) => {
                        if (results.result.succeeded) {
                            this.ResetForm.reset();
                            this.router.navigate(['auth/login']).then(() => {
                                this.toastr.success("Password Changed!");
                            });
                        }
                        else {
                            results.result.errors.forEach(element => {
                                switch (element.code) {
                                    case 'EmailDoesntExist':
                                        this.toastr.error("This email address doesnt exist", "Please register an account!")
                                        this.ResetForm.reset();
                                        this.router.navigate(['auth/register']);
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
                                        this.toastr.error("If this error persists please contact an admin.", "An error occured the password reset!");
                                        break;
                                }
                            });

                        }
                    }
                );
        } else if (this.ResetForm.value["Password"] != this.ResetForm.value["PasswordConfirm"]) {
            this.toastr.error("Passwords must match!");
        }

    }

    ngOnInit() {

    }
}
