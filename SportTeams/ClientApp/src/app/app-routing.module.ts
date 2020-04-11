import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';

import { LoginComponent } from './auth/login.component';
import { RegisterComponent } from './auth/register.component';
import { VerifyAccountComponent } from './auth/verifyaccount.component';
import { ForgotPasswordComponent } from './auth/forgotpassword.component';
import { ResetPasswordComponent } from './auth/resetpassword.component';
import { AuthGuard } from './auth/Guards/AuthenticationGuard';
import { LogoutComponent } from './auth/logout.component';

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

    { path: 'auth/login', component: LoginComponent },
    { path: 'auth/logout', component: LogoutComponent },
    { path: 'auth/register', component: RegisterComponent },
    { path: 'auth/verify-account', component: VerifyAccountComponent },
    { path: 'auth/forgot-password', component: ForgotPasswordComponent },
    { path: 'auth/reset-password', component: ResetPasswordComponent },

    { path: 'person', component: PersonIndexComponent, canActivate: [AuthGuard] },
    { path: 'person/add', component: PersonAddComponent, canActivate: [AuthGuard] },
    { path: 'person/update/:id', component: PersonUpdateComponent, canActivate: [AuthGuard] },
    { path: 'person/delete/:id', component: PersonDeleteComponent, canActivate: [AuthGuard] },

    { path: 'team', component: TeamIndexComponent, canActivate: [AuthGuard] },
    { path: 'team/add', component: TeamAddComponent, canActivate: [AuthGuard] },
    { path: 'team/update/:id', component: TeamUpdateComponent, canActivate: [AuthGuard] },
    { path: 'team/delete/:id', component: TeamDeleteComponent, canActivate: [AuthGuard] },

    { path: 'member/add/:id', component: MemberAddComponent },
    { path: 'member/delete/:teamid/:personid', component: MemberDeleteComponent },

    { path: '**', redirectTo: '/' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
