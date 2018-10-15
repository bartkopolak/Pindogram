import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';
import { SERVER_API_URL } from '../../app.constants';

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
}
