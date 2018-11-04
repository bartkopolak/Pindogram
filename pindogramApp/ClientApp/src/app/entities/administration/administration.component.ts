import { AuthGuard } from './../../shared/auth.guard';
import { UserService } from './../../shared/users/users.service';
import { Subscription } from 'rxjs/Subscription';
import { MessageService } from './../../shared/message.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { MemesService } from '../../home';
import { Memes } from '../../home/memes/memes';
import { AlertService } from '../../shared/alert/alert.service';
import { DeleteMemesComponent } from '../../home/memes/delete-memes.component';
import { User } from '../../shared';

@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: [
    './administration.component.scss'
  ]
})

export class AdministrationComponent implements OnInit, OnDestroy {

  memes: Memes[];
  users: User[];
  subscription: Subscription;
  memeActivated = [];
  memeRates: any = {};
  viewBar: any[] = [200, 400];
  viewLine: any[] = [1000, 400];
  showXAxis = true;
  showYAxis = true;
  showXAxisLabel = true;
  showYAxisLabel = true;
  showGridLines = false;
  colorScheme = {
    domain: ['#5AA454', '#A10A28']
  };

  groups = [
    {id: 'ADMIN', name: 'Administrator'},
    {id: 'USER', name: 'Użytkownik'}
  ];

  constructor(
    private memesService: MemesService,
    private alertService: AlertService,
    private modalService: NgbModal,
    private messageService: MessageService,
    private userService: UserService,
    private authenticate: AuthGuard
  ) {
    this.subscription = this.messageService.getMessage().subscribe(message => {
      this.loadUnapproved();
    });
  }

  loadApprovedMemesChartData() {
    this.memesService.getDateToNumberOfApprovedMemes().subscribe(
      (res: HttpResponse<any>) => {
        const resData = res.body;
        this.memeActivated = [
          {
            name: 'Zatwierdzone memy',
            series: resData
          }
        ];
      },
      (res: HttpErrorResponse) => this.onError(res.message)
    );
  }

  loadLikeDislikeChartData() {
    this.memesService.getNumerOfAllLikesAndDislikes().subscribe(
      (res: HttpResponse<any>) => {
        this.memeRates = res.body;
      },
      (res: HttpErrorResponse) => this.onError(res.message)
    );
  }

  loadUnapproved() {
    this.memesService.getAllUnapproved().subscribe(
      (res: HttpResponse<Memes[]>) => {
        this.memes = res.body;
      },
      (res: HttpErrorResponse) => this.onError(res.message)
    );
  }

  loadUsers() {
    this.userService.getAll().subscribe(
      (response) => {
        this.users = response;
      },
      (res: HttpErrorResponse) => this.onError(res.message)
    );
  }

  loadAll() {
    this.loadUsers();
    this.loadUnapproved();
    this.loadLikeDislikeChartData();
    this.loadApprovedMemesChartData();
  }

  changeUserStatus(user: User) {
    if (user.isActive) {
      this.userService.deactiveUser(user.id).subscribe(
        (res: HttpResponse<any>) => {
          this.onSuccess('Użytkownik został dezaktywowany');
          this.loadUsers();
        },
        (res: HttpErrorResponse) => this.onError(res.message)
      );
    } else {
      this.userService.activeUser(user.id).subscribe(
        (res: HttpResponse<any>) => {
          this.onSuccess('Użytkownik został aktywowany');
          this.loadUsers();
        },
        (res: HttpErrorResponse) => this.onError(res.message)
      );
    }
  }

  changeUserGroup(user: User) {
    switch (user.group) {
      case 'ADMIN':
        this.userService.addUserToAdminGroup(user.id).subscribe(
          (res: HttpResponse<any>) => {
            this.onSuccess(res.body.message);
            this.loadUsers();
          },
          (res: HttpErrorResponse) => this.onError(res.message)
        );
        break;
        case 'USER':
        this.userService.addUserToUserGroup(user.id).subscribe(
          (res: HttpResponse<any>) => {
            this.onSuccess(res.body.message);
            this.loadUsers();
          },
          (res: HttpErrorResponse) => this.onError(res.message)
        );
        break;
      default:
        this.onError('Brak grupy!');
        break;
    }
  }

  deleteUser(id: number) {
    this.userService.delete(id).subscribe(
      response => {
        this.loadUsers();
      }
    );
  }

  approveMeme(id: number) {
    this.memesService.approveMeme(id).subscribe((response) => {
      this.alertService.success(response.body.message);
      this.loadUnapproved();
      this.loadApprovedMemesChartData();
    },
    error => {
      this.alertService.error(error);
    });
  }

  deleteMeme(meme: Memes) {
    const modalRef = this.modalService.open(DeleteMemesComponent as Component, {centered: true});
    modalRef.componentInstance.meme = meme;
    modalRef.result.then(() => {
      this.loadUnapproved();
    }, cancel => {});
  }

  isCurrentUser() {
    return this.authenticate.hasAnyAuthority().id;
  }

  trackId(index: number, item: Memes) {
    return item.id;
  }

  userId(index: number, item: User) {
    return item.id;
  }

  ngOnInit() {
    this.loadAll();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  private onSuccess(message) {
    this.alertService.success(message);
  }

  private onError(error) {
    this.alertService.error(error);
  }

}
