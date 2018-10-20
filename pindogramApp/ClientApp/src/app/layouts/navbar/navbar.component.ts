import { MessageService } from './../../shared/message.service';
import { AuthGuard } from './../../shared/auth.guard';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../shared';
import { AddMemesComponent } from '../../home/memes/add-memes.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: [
        'navbar.scss'
    ]
})
export class NavbarComponent implements OnInit {
  user: User;

  constructor(private authenticate: AuthGuard,
     private router: Router,
     private modalService: NgbModal,
     private messageService: MessageService
     ) {}

  addMeme() {
    this.modalService.open(AddMemesComponent as Component, {centered: true})
    .result.then((result) => {
      this.messageService.sendMessage(null);
    }, cancel => {});
  }

  isAuthenticated() {
    return this.authenticate.isAuthenticated();
  }

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('currentUser'));
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.router.navigate(['/login']);
  }

}
