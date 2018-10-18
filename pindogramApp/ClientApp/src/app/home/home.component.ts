import { Component, OnInit } from '@angular/core';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Memes } from './memes/memes';
import { MemesService } from './memes/memes.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertService } from './../shared/alert/alert.service';
import { AddMemesComponent } from './memes/add-memes.component';
import { DeleteMemesComponent } from './memes/delete-memes.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: [
    './home.component.scss'
  ]
})

export class HomeComponent implements OnInit {

  memes: Memes[];
  constructor(private router: Router,
    private memesService: MemesService,
    private alertService: AlertService,
    private modalService: NgbModal) {}

  loadAll() {
    this.memesService.getAll().subscribe(
      (res: HttpResponse<Memes[]>) => {
        this.memes = res.body;
      },
      (res: HttpErrorResponse) => this.onError(res.message)
    );
  }

  addMeme() {
    this.modalService.open(AddMemesComponent as Component, {centered: true})
    .result.then((result) => {
      this.loadAll();
    }, cancel => {});
  }

  upvoteMeme(id: number) {
    this.memesService.upvote(id).subscribe(() => {
      this.loadAll();
    },
    error => {
      this.alertService.error(error);
    });
  }

  downvoteMeme(id: number) {
    this.memesService.downvote(id).subscribe(() => {
      this.loadAll();
    },
    error => {
      this.alertService.error(error);
    });
  }

  deleteMeme(meme: Memes) {
    const modalRef = this.modalService.open(DeleteMemesComponent as Component, {centered: true});
    modalRef.componentInstance.meme = meme;
    modalRef.result.then(() => {
      this.loadAll();
    }, cancel => {});
  }

  logout() {
      localStorage.removeItem('currentUser');
      this.router.navigate(['/login']);
  }

  trackId(index: number, item: Memes) {
    return item.id;
  }

  ngOnInit() {
    this.loadAll();
  }

  private onError(error) {
    this.alertService.error(error);
  }

}
