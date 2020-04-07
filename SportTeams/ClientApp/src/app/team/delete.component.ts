import { Component, Inject, AfterViewInit, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-team-delete',
    template: ''
})
export class TeamDeleteComponent implements AfterViewInit {

    constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private router: Router, private toastr: ToastrService,
        private route: ActivatedRoute) {

    }

    ngAfterViewInit() {

        let header = new HttpHeaders();
        header.append('Content-type', 'application/json');
        this.http.delete(this.baseUrl + 'api/team/' + this.route.snapshot.params.id, { headers: header }).toPromise().then(
            (result: any) => {
                if (result.success) {
                    this.router.navigate(['team']).then(() => {
                        this.toastr.success(result.msg, "Delete Successful!");
                    });
                }
                else {
                    this.toastr.success(result.msg, "Delete Failed!");
                }
            });
    }

}

