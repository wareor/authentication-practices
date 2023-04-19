import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { catchError, Observable } from "rxjs";
@Injectable()
export class AuthService {
    constructor(private http: HttpClient) {

    }
    login(Email: string, Password: string): Observable<UserSession> {
        return this.http.post<UserSession>("https://localhost:7262/User/authenticate", { Email, Password })
            .pipe();
    }
}

export interface User {
    Email: string;
    Password: string;
}

export interface UserSession {
    token: string,
    userInfo: {
        Id: string,
        Name: string,
        Email: string,
        Password: string
    }
}