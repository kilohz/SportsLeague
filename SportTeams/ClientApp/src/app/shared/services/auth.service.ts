import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class User {
    id: number;
    email: string;
    username: string;
    fullName: string;
    token?: string;
    roles: string[];
}

@Injectable({ providedIn: 'root' })
export class AuthService {

    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient, private toastr: ToastrService) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    LoginUser(loginForm: FormGroup): any {
        return this.http.post<any>("/api/auth/login", loginForm.value)
            .pipe(map(results => {
                // login successful if there's a jwt token in the response
                if (results.result.user && results.result.user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(results.result.user));
                    this.currentUserSubject.next(results.result.user);
                    this.toastr.success("Login Successful!");
                }

                return results;
            }));
    }

    logout() {
        debugger;
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
        let header = new HttpHeaders();
        header.append('Content-type', 'application/json');
        return this.http.post<any>("/api/auth/logout", {}, { headers: header });
    }

    RegisterUser(registerForm: FormGroup): any {
        let header = new HttpHeaders();
        header.append('Content-type', 'application/json');

        //Api Post
        return this.http.post("/api/auth/Register", registerForm.value, { headers: header });
    }

    comparePasswords(registerForm: FormGroup): boolean {
        return registerForm.get('Password').value == registerForm.get('ConfirmPassword').value;
    }

    VerifyAccount(body: any): any {
        let header = new HttpHeaders();
        header.append('Content-type', 'application/json');
        return this.http.post("/api/auth/verifyaccount", body, { headers: header });
    }

    ResetPassword(Password: string, Email: string, Token: string) {
        let header = new HttpHeaders();
        header.append('Content-type', 'application/json');
        return this.http.post("/api/auth/ResetPassword", { Password: Password, Email: Email, Token: Token }, { headers: header });
    }

    ForgotPassword(Email: string) {
        let header = new HttpHeaders();
        header.append('Content-type', 'application/json');
        return this.http.get("/api/auth/ForgotPassword?Email=" + Email, { headers: header });
    }
}
