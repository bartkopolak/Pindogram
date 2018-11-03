import { Injectable } from '@angular/core';
import { User } from './user';
import { SERVER_API_URL } from '../../app.constants';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/observable';

@Injectable()
export class UserService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<User[]>(SERVER_API_URL + `/api/Users`);
    }

    getById(id: number) {
        return this.http.get(SERVER_API_URL + `/api/Users/` + id);
    }

    register(user: User) {
        return this.http.post(SERVER_API_URL + `/api/Users/register`, user);
    }

    update(user: User) {
        return this.http.put(SERVER_API_URL + `/api/Users/` + user.id, user);
    }

    delete(id: number) {
        return this.http.delete(SERVER_API_URL + `/api/Users/` + id);
    }

    activeUser(id: Number): Observable<HttpResponse<any>>  {
      return this.http.post<any>(SERVER_API_URL + `/api/Users/ActiveUser/` + id, {}, { observe: 'response'});
    }

    deactiveUser(id: Number): Observable<HttpResponse<any>>  {
      return this.http.post<any>(SERVER_API_URL + `/api/Users/DeactiveUser/` + id, {}, { observe: 'response'});
    }

    addUserToUserGroup(userId: Number): Observable<HttpResponse<any>> {
      return this.http.post<any>(SERVER_API_URL + `/api/Users/AddUserToUserGroup/` + userId, {}, { observe: 'response'});
    }

    addUserToAdminGroup(userId: Number): Observable<HttpResponse<any>> {
      return this.http.post<any>(SERVER_API_URL + `/api/Users/AddUserToAdminGroup/` + userId, {}, { observe: 'response'});
    }
}
