import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-root',
    template: ''
})

export class VerifyAccountComponent implements OnInit {

    Id: string;
    Token: string;

    constructor(private http: HttpClient, private route: ActivatedRoute, public userService: AuthService,
        private router: Router, private toastr: ToastrService) {
        this.route.queryParams.subscribe(params => {
            this.Id = params['Id'];
            this.Token = params['Token'];
        });


    }

    ngOnInit() {
        this.userService.VerifyAccount({ Id: this.Id, Token: this.Token })
            .toPromise().then(
                (result: any) => {
                    if (result) {
                        this.toastr.success("The user will now be able to log into their account.", "Verification Successful!");
                        this.router.navigate(['auth/login']);
                    } else {
                        this.toastr.error("Something has gone wrong with the email confirmation.", "Verification Failed!");
                        this.router.navigate(['auth/login']);
                    }
                });
    }
}
