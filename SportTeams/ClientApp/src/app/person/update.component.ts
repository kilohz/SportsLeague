import { Component, Inject, AfterViewInit, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-person-update',
    templateUrl: './update.component.html'
})
export class PersonUpdateComponent implements AfterViewInit {

    public person: Person;

    UpdateForm = new FormGroup({
        Id: new FormControl('', null),
        CreatedOn: new FormControl('', null),
        Name: new FormControl('', Validators.required),
        Email: new FormControl('', Validators.compose(
            [Validators.email, Validators.required])),
    });;

    constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private router: Router, private toastr: ToastrService,
        private route: ActivatedRoute) {
        
    }

    ngAfterViewInit() {
        this.http.get<Person>(this.baseUrl + 'api/person/' + this.route.snapshot.params.id).subscribe(result => {
            this.person = result;
            this.UpdateForm.controls["Id"].setValue(this.person.id);
            this.UpdateForm.controls["Name"].setValue(this.person.name);
            this.UpdateForm.controls["Email"].setValue(this.person.email); 
            this.UpdateForm.controls["CreatedOn"].setValue(this.person.createdOn); 
        }, error => console.error(error));
    }

    onSubmit(UpdateForm: FormGroup) {
        this.UpdateForm.markAsTouched();
        if (UpdateForm.valid) {

            //Update person

            let header = new HttpHeaders();
            header.append('Content-type', 'application/json');
            this.http.put(this.baseUrl + 'api/person', UpdateForm.value, { headers: header }).toPromise().then(
                (result: any) => {
                    if (result.success) {
                        this.router.navigate(['person']).then(() => {
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

interface Person {
    name: string;
    email: number;
    id: number;
    createdOn: Date;
    scores: any[];
    members: any[];
}
