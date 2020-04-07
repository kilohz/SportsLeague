import { Component, Inject, AfterViewInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-person-index',
    templateUrl: './index.component.html'
})
export class PersonIndexComponent implements AfterViewInit {

    ngAfterViewInit() {
        this.http.get<Person[]>(this.baseUrl + 'api/person').subscribe(result => {
            this.people = result;
            this.loaded = true;
        }, error => this.toastr.error("Couldn't load players.", "Load Failed!"));
    }

    public loaded: boolean;
    public people: Person[];

    constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private toastr: ToastrService) {


    }

}

interface Person {
    name: string;
    email: number;
    id: number;
}
