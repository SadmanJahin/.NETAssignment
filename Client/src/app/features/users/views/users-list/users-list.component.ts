import { Component, inject } from '@angular/core';
import { UserService } from '../../services/user.service';
import { UserDto } from '../../models/user';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-users-list',
  imports: [],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css'
})
export class UsersListComponent {
  private userService = inject(UserService)

  ngOnInit(): void {
  }

  getUsers() {
    this.userService.getUsers().subscribe({
      next: (resp: UserDto[]) => {
        console.log(resp);
      },
      error: (err: HttpErrorResponse) => {

      }
    })
  }
}
