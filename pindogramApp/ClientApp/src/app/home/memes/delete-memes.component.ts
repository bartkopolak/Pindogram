import { MemesService } from './memes.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, Input } from '@angular/core';
import { AlertService } from '../../shared/alert/alert.service';
import { Memes } from './memes';

@Component({
  selector: 'app-delete-memes',
  templateUrl: './delete-memes.component.html'
})

export class DeleteMemesComponent {
  @Input() meme: Memes;

  constructor(private activeModal: NgbActiveModal,
    private memesService: MemesService,
    private alertService: AlertService) {}

  delete() {
    this.memesService.delete(this.meme.id).subscribe(() => {
      this.alertService.success('Pomyślnie usunięto mema.');
      this.activeModal.close('cancel');
    },
    error => {
      this.alertService.error(error);
      this.activeModal.dismiss('cancel');
    });
  }

  cancel() {
    this.activeModal.dismiss('cancel');
  }
}


