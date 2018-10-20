import { Memes } from './memes';
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/observable';
import { SERVER_API_URL } from '../../app.constants';

@Injectable()
export class MemesService {
    constructor(private http: HttpClient) { }

    getAllApproved(): Observable<HttpResponse<Memes[]>> {
      return this.http.get<Memes[]>(SERVER_API_URL + `/api/Memes/GetAllApprovedMemes`, { observe: 'response' })
        .map((res: HttpResponse<Memes[]>) => this.convertArrayResponse(res));
    }

    getAllUnapproved(): Observable<HttpResponse<Memes[]>> {
      return this.http.get<Memes[]>(SERVER_API_URL + `/api/Memes/GetAllUnapprovedMemes`, { observe: 'response' })
        .map((res: HttpResponse<Memes[]>) => this.convertArrayResponse(res));
    }

    create(meme: Memes): Observable<HttpResponse<Memes>> {
      const copy = this.convert(meme);
      return this.http.post<Memes>(SERVER_API_URL + `/api/Memes/CreateMeme`, copy, { observe: 'response' })
        .map((res: HttpResponse<Memes>) => this.convertResponse(res));
    }

    approveMeme(id: number): Observable<HttpResponse<any>> {
      return this.http.post<any>(SERVER_API_URL + `/api/Memes/ApproveMeme/` + id, {}, { observe: 'response'});
    }

    upvote(id: number): Observable<HttpResponse<any>> {
      return this.http.post<any>(SERVER_API_URL + `/api/Memes/UpvoteMeme?memeId=` + id, {}, { observe: 'response'});
    }

    downvote(id: number): Observable<HttpResponse<any>> {
      return this.http.post<any>(SERVER_API_URL + `/api/Memes/DownvoteMeme?memeId=` + id, {}, { observe: 'response'});
    }

    delete(id: number): Observable<HttpResponse<any>> {
      return this.http.delete<any>(SERVER_API_URL + `/api/Memes/DeleteMemeById/` + id, { observe: 'response'});
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
