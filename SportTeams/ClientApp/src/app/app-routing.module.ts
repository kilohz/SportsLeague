import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';

import { PersonIndexComponent } from './person/index.component';
import { PersonAddComponent } from './person/add.component';
import { PersonUpdateComponent } from './person/update.component';
import { PersonDeleteComponent } from './person/delete.component';

import { TeamIndexComponent } from './team/index.component';
import { TeamAddComponent } from './team/add.component';
import { TeamUpdateComponent } from './team/update.component';
import { TeamDeleteComponent } from './team/delete.component';

import { MemberAddComponent } from './member/add.component';
import { MemberDeleteComponent } from './member/delete.component';

const routes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },

    { path: 'person', component: PersonIndexComponent },
    { path: 'person/add', component: PersonAddComponent },
    { path: 'person/update/:id', component: PersonUpdateComponent },
    { path: 'person/delete/:id', component: PersonDeleteComponent },

    { path: 'team', component: TeamIndexComponent },
    { path: 'team/add', component: TeamAddComponent },
    { path: 'team/update/:id', component: TeamUpdateComponent },
    { path: 'team/delete/:id', component: TeamDeleteComponent },

    { path: 'member/add/:id', component: MemberAddComponent },
    { path: 'member/delete/:teamid/:personid', component: MemberDeleteComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
