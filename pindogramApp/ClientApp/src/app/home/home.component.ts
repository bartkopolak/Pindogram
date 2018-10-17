import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertService } from './../shared/alert/alert.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MemesService } from './memes/memes.service';
import { Memes } from './memes/memes';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { AddMemesComponent } from './memes/add-memes.component';

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
    });
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
