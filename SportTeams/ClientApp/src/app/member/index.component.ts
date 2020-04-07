import { Component, Inject, AfterViewInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-member-index',
    templateUrl: './index.component.html'
})
export class MemberIndexComponent implements AfterViewInit {

    ngAfterViewInit() {
        this.http.get<Member[]>(this.baseUrl + 'api/Member/' + this.teamId).subscribe(result => {
            this.members = result;
            this.loaded = true;
        }, error => this.toastr.error("Couldn't load Members.", "Load Failed!"));
    }

    public members: Member[];
    public loaded: boolean;
    @Input() public teamId: number;

    constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private toastr: ToastrService) {


    }

}

interface Member {
    name: string;
    id: number;
}
