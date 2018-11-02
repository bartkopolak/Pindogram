import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { DeleteMemesComponent } from './memes/delete-memes.component';
import { MemesService } from './memes/memes.service';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HomeComponent } from './home.component';
import { SharedModule } from '../shared/shared.module';
import { AddMemesComponent } from './memes/add-memes.component';
import { DetailMemeComponent } from './memes/detail-meme.component';

@NgModule({
  imports: [
    SharedModule
  ],
  declarations: [
    HomeComponent,
    AddMemesComponent,
    DeleteMemesComponent,
    DetailMemeComponent
  ],
  entryComponents: [
    AddMemesComponent,
    DeleteMemesComponent
  ],
  providers: [
    MemesService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class HomeModule {}
