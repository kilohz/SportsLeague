import { Component, Inject, AfterViewInit, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-member-delete',
    template: ''
})
export class MemberDeleteComponent implements AfterViewInit {

    constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private router: Router, private toastr: ToastrService,
        private route: ActivatedRoute) {

        this.personId = this.route.snapshot.params.personid;
        this.teamId = this.route.snapshot.params.teamid;
    }

    private personId: number;
    private teamId: number;


    ngAfterViewInit() {

        let header = new HttpHeaders();
        header.append('Content-type', 'application/json');
        this.http.delete(this.baseUrl + 'api/member/?personId=' + this.personId + '&teamId=' + this.teamId, { headers: header }).toPromise().then(
            (result: any) => {
                if (result.success) {
                    this.router.navigate(['member']).then(() => {
                        this.toastr.success(result.msg, "Delete Successful!");
                    });
                }
                else {
                    this.toastr.success(result.msg, "Delete Failed!");
                }
            });
    }

}

