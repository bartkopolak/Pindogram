import { AlertService } from './../shared/alert/alert.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MemesService } from './memes/memes.service';
import { Memes } from './memes/memes';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: [
    './home.component.scss'
  ]
})

export class HomeComponent implements OnInit {

  // imageSrc: any = '';
  // memes: Memes;
  memes: Memes[];
  constructor(private router: Router,
    private memesService: MemesService,
    private alertService: AlertService) {}

  ngOnInit() {
    this.memesService.getAll().subscribe(
      (res: HttpResponse<Memes[]>) => {
        console.log('res', res);
        this.memes = res.body;
      },
      (res: HttpErrorResponse) => this.onError(res.message)
    );
  }

  // handleInputChange(e) {
  //   const file = e.dataTransfer ? e.dataTransfer.files[0] : e.target.files[0];
  //   const pattern = /image-*/;
  //   const reader = new FileReader();
  //   if (!file.type.match(pattern)) {
  //     alert('invalid format');
  //     return;
  //   }
  //   reader.onload = this._handleReaderLoaded.bind(this);
  //   reader.readAsDataURL(file);
  // }

  // _handleReaderLoaded(e) {
  //   const reader = e.target;
  //   this.imageSrc = reader.result;
  //   const strImage = this.imageSrc.replace(/^data:image\/[a-z]+;base64,/, '');
  //   this.memes = {
  //     title: 'Siema',
  //     image: strImage
  //   };
  //   this.memesService.create(this.memes).subscribe(() => {
  //     console.log('siema');
  //   });
  // }

  trackId(index: number, item: Memes) {
    return item.id;
  }

  private onError(error) {
    this.alertService.error(error);
  }

  logout() {
      localStorage.removeItem('currentUser');
      this.router.navigate(['/login']);
  }
}
