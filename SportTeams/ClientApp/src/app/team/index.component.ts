import { Component, Inject, AfterViewInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-team-index',
  templateUrl: './index.component.html'
})
export class TeamIndexComponent implements AfterViewInit{

  ngAfterViewInit() {
    this.http.get<Team[]>(this.baseUrl + 'api/team').subscribe(result => {
        this.teams = result;
        this.loaded = true;
    }, error => this.toastr.error("Couldn't load teams.", "Load Failed!")  );
  }

    public teams: Team[];
    public loaded: boolean;

    constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private toastr: ToastrService) {
    

  }

}

interface Team {
  name: string;
  id: number;
}
