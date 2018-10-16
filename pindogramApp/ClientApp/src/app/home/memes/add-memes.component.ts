import { MemesService } from './memes.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component } from '@angular/core';
import { Memes } from './memes';
import { AlertService } from '../../shared/alert/alert.service';

@Component({
  selector: 'app-add-memes',
  templateUrl: './add-memes.component.html'
})

export class AddMemesComponent {

  imageSrc: any = '';
  meme: Memes = {
    title: null,
    image: null
  };
  loading = false;

  constructor(private activeModal: NgbActiveModal,
    private memesService: MemesService,
    private alertService: AlertService) {}

  handleInputChange(e) {
    this.loading = false;
    const file = e.dataTransfer ? e.dataTransfer.files[0] : e.target.files[0];
    const pattern = /image-*/;
    const reader = new FileReader();
    if (!file) {
      this.meme.image = null;
      return;
    } else if ( !file.type.match(pattern)) {
      this.loading = true;
      return;
    }
    this._handleReaderLoaded.bind(this);
    reader.onload = this._handleReaderLoaded.bind(this);
    reader.readAsDataURL(file);
  }

  _handleReaderLoaded(e) {
    const reader = e.target;
    this.meme.image = reader.result.replace(/^data:image\/[a-z]+;base64,/, '');
  }

  save() {
    this.loading = true;
    this.memesService.create(this.meme).subscribe(
    data => {
      this.alertService.success('Pomyślnie dodano mema, oczekuje na akceptację.');
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


