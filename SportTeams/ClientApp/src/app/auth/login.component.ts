import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../shared/services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { AppComponent } from '../app.component';

@Component({
    selector: 'app-root',
    templateUrl: './Login.component.html'
})

export class LoginComponent implements OnInit {
    loading = false;
    Submitted = false;
    returnUrl: string;
    ErrorMessage = '';

    LoginForm = new FormGroup({
        Email: new FormControl('', Validators.email),
        Password: new FormControl('', Validators.required)
    });

    constructor(public authenticationService: AuthService, private router: Router,
        private route: ActivatedRoute, private toastr: ToastrService, private appComponent: AppComponent) {
    }

    onSubmit(loginForm: FormGroup) {
        this.LoginForm.markAsTouched();
        this.Submitted = true;

        this.authenticationService.LoginUser(loginForm)
            .pipe(first())
            .toPromise().then(
                (results: any) => {
                    if (results.result.succeeded) {
                        this.LoginForm.reset();
                        if (this.returnUrl != "") {
                            this.router.navigate([this.returnUrl]);
                        } else {
                            this.router.navigate(["/"]);
                        }
                    }
                    else {
                        results.result.errors.forEach(element => {
                            switch (element) {
                                case 'IncorrectDetails':
                                    this.toastr.error("Please try a different password.", "Incorrect login details!");
                                    break;
                                default:
                                    this.toastr.error("If this error persists please contact an admin.", "An error occured while logging in!");
                                    break;
                            }
                        });

                    }
                }
            );


    }

    ngOnInit() {
        this.authenticationService.logout();
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }
}
