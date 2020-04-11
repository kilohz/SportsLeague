import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-root',
    template: ''
})

export class LogoutComponent implements OnInit {

    constructor(private http: HttpClient, private route: ActivatedRoute, public userService: AuthService,
        private router: Router, private toastr: ToastrService) {

    }

    ngOnInit() {
        debugger;
        this.userService.logout()
            .toPromise().then(
                (result: any) => {
                    if (result) {
                        this.toastr.success("You have been logged out.", "Logged out.");
                        this.router.navigate(['auth/login']);
                    } else {
                        this.toastr.error("Something has gone wrong with logging you out.", "Not Logged out!");
                        this.router.navigate(['/']);
                    }
                });
    }
}
