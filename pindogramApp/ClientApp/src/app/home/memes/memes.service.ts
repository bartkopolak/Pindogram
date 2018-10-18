import { Memes } from './memes';
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/observable';
import { SERVER_API_URL } from '../../app.constants';

@Injectable()
export class MemesService {
    constructor(private http: HttpClient) { }

    getAll(): Observable<HttpResponse<Memes[]>> {
      return this.http.get<Memes[]>(SERVER_API_URL + `/api/Memes`, { observe: 'response' })
        .map((res: HttpResponse<Memes[]>) => this.convertArrayResponse(res));
    }

    create(meme: Memes): Observable<HttpResponse<Memes>> {
      const copy = this.convert(meme);
      return this.http.post<Memes>(SERVER_API_URL + `/api/Memes/createMeme`, copy, { observe: 'response' })
        .map((res: HttpResponse<Memes>) => this.convertResponse(res));
    }

    delete(id: number): Observable<HttpResponse<any>> {
      return this.http.delete<any>(SERVER_API_URL + `/api/Memes/` + id, { observe: 'response'});
    }

    private convertResponse(res: HttpResponse<Memes>): HttpResponse<Memes> {
      const body: Memes = this.convertItemFromServer(res.body);
      return res.clone({body});
    }

    private convertArrayResponse(res: HttpResponse<Memes[]>): HttpResponse<Memes[]> {
      const jsonResponse: Memes[] = res.body;
      const body: Memes[] = [];
      for (let i = 0; i < jsonResponse.length; i++) {
          body.push(this.convertItemFromServer(jsonResponse[i]));
      }
      return res.clone({body});
    }

    private convertItemFromServer(memes: Memes): Memes {
      const copy: Memes = Object.assign({}, memes);
      return copy;
    }

    private convert(memes: Memes): Memes {
      const copy: Memes = Object.assign({}, memes);
      return copy;
    }
}
