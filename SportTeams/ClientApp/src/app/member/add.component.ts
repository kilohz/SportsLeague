import { Component, Inject, AfterViewInit, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-member-add',
    templateUrl: './add.component.html'
})
export class MemberAddComponent implements OnInit {

    CreateForm = new FormGroup({
        Name: new FormControl('', Validators.required)
    });

    Submitted = false;

    constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private router: Router, private toastr: ToastrService) {

    }

    onSubmit(createForm: FormGroup) {
        this.CreateForm.markAsTouched();
        if (createForm.valid) {

            //create member

            let header = new HttpHeaders();
            header.append('Content-type', 'application/json');
            this.http.post(this.baseUrl + 'api/member', createForm.value, { headers: header }).toPromise().then(
                (result: any) => {
                    if (result.success) {
                        this.CreateForm.reset();
                        this.router.navigate(['member']).then(() => {
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
        this.CreateForm.reset();
    }
}
