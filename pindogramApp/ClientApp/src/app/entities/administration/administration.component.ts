import { Subscription } from 'rxjs/Subscription';
import { MessageService } from './../../shared/message.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { MemesService } from '../../home';
import { Memes } from '../../home/memes/memes';
import { AlertService } from '../../shared/alert/alert.service';
import { DeleteMemesComponent } from '../../home/memes/delete-memes.component';
@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: [
    './administration.component.scss'
  ]
})

export class AdministrationComponent implements OnInit, OnDestroy {

  memes: Memes[];
  subscription: Subscription;

  constructor(
    private memesService: MemesService,
    private alertService: AlertService,
    private modalService: NgbModal,
    private messageService: MessageService
  ) {
    this.subscription = this.messageService.getMessage().subscribe(message => {
      this.loadAll();
    });
  }

  loadAll() {
    this.memesService.getAllUnapproved().subscribe(
      (res: HttpResponse<Memes[]>) => {
        this.memes = res.body;
      },
      (res: HttpErrorResponse) => this.onError(res.message)
    );
  }

  approveMeme(id: number) {
    this.memesService.approveMeme(id).subscribe((response) => {
      this.alertService.success(response.body.message);
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

  trackId(index: number, item: Memes) {
    return item.id;
  }

  ngOnInit() {
    this.loadAll();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  private onError(error) {
    this.alertService.error(error);
  }

}
