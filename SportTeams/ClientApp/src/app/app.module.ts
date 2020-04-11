import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { LoginComponent } from './auth/login.component';
import { LogoutComponent } from './auth/logout.component';
import { RegisterComponent } from './auth/register.component';
import { VerifyAccountComponent } from './auth/verifyaccount.component';
import { ForgotPasswordComponent } from './auth/forgotpassword.component';
import { ResetPasswordComponent } from './auth/resetpassword.component';
import { JwtInterceptor } from './auth/interceptors/JwtInterceptor';
import { ErrorInterceptor } from './auth/interceptors/ErrorInterceptor';
import { AuthService } from './shared/services/auth.service';

import { PersonIndexComponent } from './person/index.component';
import { PersonAddComponent } from './person/add.component';
import { PersonUpdateComponent } from './person/update.component';
import { PersonDeleteComponent } from './person/delete.component';

import { TeamIndexComponent } from './team/index.component';
import { TeamAddComponent } from './team/add.component';
import { TeamUpdateComponent } from './team/update.component';
import { TeamDeleteComponent } from './team/delete.component';

import { MemberIndexComponent } from './member/index.component';
import { MemberAddComponent } from './member/add.component';
import { MemberDeleteComponent } from './member/delete.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,

        LoginComponent,
        LogoutComponent,
        RegisterComponent,
        VerifyAccountComponent,
        ForgotPasswordComponent,
        ResetPasswordComponent,

        PersonIndexComponent,
        PersonAddComponent,
        PersonUpdateComponent,
        PersonDeleteComponent,

        TeamIndexComponent,
        TeamAddComponent,
        TeamUpdateComponent,
        TeamDeleteComponent,

        MemberIndexComponent,
        MemberAddComponent,
        MemberDeleteComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        ToastrModule.forRoot(),
        BrowserAnimationsModule,
        AppRoutingModule
    ],
    providers: [ AuthService, 
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }],
    bootstrap: [AppComponent]
})
export class AppModule { }
