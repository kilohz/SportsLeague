import { Component, Inject, AfterViewInit, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-member-add',
    templateUrl: './add.component.html'
})
export class MemberAddComponent implements OnInit {

    AddForm = new FormGroup({
        PersonId: new FormControl('', Validators.required),
        TeamId: new FormControl('', null)
    });

    Submitted = false;
    teamId: number;
    public people: Person[];

    constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private router: Router, private toastr: ToastrService,
        private route: ActivatedRoute) {

    }

    onSubmit(addForm: FormGroup) {
        this.AddForm.markAsTouched();
        if (addForm.valid) {
            //create member
            let header = new HttpHeaders();
            header.append('Content-type', 'application/json');

            //parse form ints
            addForm.value.PersonId = parseFloat(addForm.value.PersonId);
            addForm.value.TeamId = parseFloat(addForm.value.TeamId);

            this.http.post(this.baseUrl + 'api/member', addForm.value, { headers: header }).toPromise().then(
                (result: any) => {
                    if (result.success) {
                        this.AddForm.reset();
                        this.router.navigate(['team/update', this.teamId]).then(() => {
                            this.toastr.success(result.msg, "Create Successful!");
                        });
                    }
                    else {
                        this.toastr.error(result.msg, "Create Failed!");
                    }
                });
        }
        this.Submitted = true;
    }

    ngOnInit() {
        this.AddForm.reset();
        this.teamId = this.route.snapshot.params.id;
        this.AddForm.controls["TeamId"].setValue(this.teamId);

        //get list of people, team already selected
        this.http.get<Person[]>(this.baseUrl + 'api/person').subscribe(result => {
            this.people = result;
        }, error => this.toastr.error("Couldn't load players.", "Load Failed!"));
    }
}

interface Person {
    name: string;
    email: number;
    id: number;
}
