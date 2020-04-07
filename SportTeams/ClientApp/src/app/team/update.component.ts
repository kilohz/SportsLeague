import { Component, Inject, AfterViewInit, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-team-update',
    templateUrl: './update.component.html'
})
export class TeamUpdateComponent implements AfterViewInit {

    public team: Team;
    public teamId: number;

    UpdateForm = new FormGroup({
        Id: new FormControl('', null),
        CreatedOn: new FormControl('', null),
        Name: new FormControl('', Validators.required)
    });;

    constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private router: Router, private toastr: ToastrService,
        private route: ActivatedRoute) {
        this.teamId = this.route.snapshot.params.id;
    }

    ngAfterViewInit() {
        this.http.get<Team>(this.baseUrl + 'api/team/' + this.teamId ).subscribe(result => {
            this.team = result;
            this.UpdateForm.controls["Id"].setValue(this.team.id);
            this.UpdateForm.controls["Name"].setValue(this.team.name);
            this.UpdateForm.controls["CreatedOn"].setValue(this.team.createdOn); 
        }, error => console.error(error));
    }

    onSubmit(UpdateForm: FormGroup) {
        this.UpdateForm.markAsTouched();
        if (UpdateForm.valid) {

            //Update team

            let header = new HttpHeaders();
            header.append('Content-type', 'application/json');
            this.http.put(this.baseUrl + 'api/team', UpdateForm.value, { headers: header }).toPromise().then(
                (result: any) => {
                    if (result.success) {
                        this.router.navigate(['team']).then(() => {
                            this.toastr.success(result.msg, "Update Successful!");
                        });
                    }
                    else {
                        this.toastr.error(result.msg, "Update Failed!");
                    }
                });
        }
    }
}

interface Team {
    name: string;
    id: number;
    createdOn: Date;
    scores: any[];
    members: any[];
}
