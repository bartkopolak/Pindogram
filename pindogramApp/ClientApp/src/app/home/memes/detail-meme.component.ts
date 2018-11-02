import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { MemesService } from './memes.service';
import { Component, OnInit } from '@angular/core';
import { Memes } from './memes';
import { AlertService } from '../../shared/alert/alert.service';
import { ActivatedRoute } from '@angular/router';
import { Comments } from './comments';

@Component({
  selector: 'app-detail-meme',
  templateUrl: './detail-meme.component.html',
  styleUrls: [
    './detail-meme.component.scss'
  ]
})

export class DetailMemeComponent implements OnInit {
  meme: Memes;
  commentContent = '';
  comments: Comments;

  private id = +this.route.snapshot.paramMap.get('id');
  constructor(private memesService: MemesService,
    private alertService: AlertService,
    private route: ActivatedRoute) {}

  loadAll() {
    this.memesService.getSingleApprovedMeme(this.id).subscribe(
      (res: HttpResponse<Memes>) => {
        this.meme = res.body;
        this.loadComments();
      },
      (res: HttpErrorResponse) => this.onError(res.message)
    );
  }

  loadComments() {
    this.memesService.getCommentsFromMeme(this.id).subscribe(
      (res: HttpResponse<any>) => {
        this.comments = res.body;
      },
      (res: HttpErrorResponse) => this.onError(res.message)
    );
  }

  addComment() {
    const objPost = {
      memeId: this.id,
      content: this.commentContent
    };
    this.memesService.addComment(objPost).subscribe(() => {
      this.loadComments();
      this.commentContent = '';
    },
    error => {
      this.alertService.error(error);
    });
  }

  commentToEdit(comment: any) {
    comment.toEdit = !comment.toEdit;
  }

  editComment(comment: any) {
    const objPost = {
      commentId: comment.id,
      newContent: comment.content
    };
    this.memesService.editComment(objPost).subscribe(() => {
      this.loadComments();
    },
    error => {
      this.alertService.error(error);
    });
  }

  deleteComment(id: number) {
    this.memesService.deleteComment(id).subscribe(() => {
      this.loadComments();
    },
    error => {
      this.alertService.error(error);
    });
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

  commentId(index: number, item: Comments) {
    return item.id;
  }

  ngOnInit() {
    this.loadAll();
  }

  private onError(error) {
    this.alertService.error(error);
  }
}


