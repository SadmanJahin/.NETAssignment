import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ToastCloseEvent, ToastModule } from 'primeng/toast';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { UserDto } from '../../models/user';
import { HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-user-detail',
  imports: [ReactiveFormsModule, InputTextModule, DropdownModule, CheckboxModule, CardModule, ButtonModule, DividerModule, ProgressSpinnerModule, ToastModule],
  providers: [MessageService],
  templateUrl: './user-detail.component.html',
  styleUrl: './user-detail.component.css'
})
export class UserDetailComponent {
  userForm!: FormGroup;
  genders = [{ name: 'Male', value: 'M' }, { name: 'Female', value: 'F' }, { name: 'Other', value: 'O' }];
  id: number = 0;
  requestOnProgress: boolean = false;
  isDeleted: boolean = false;
  private route = inject(ActivatedRoute);
  private formBuilder = inject(FormBuilder);
  private userService = inject(UserService);
  private router = inject(Router);
  private messageService = inject(MessageService);

  ngOnInit(): void {
    this.parseId();
    this.initializeForm();
    if (this.isUserIdExists()) {
      this.getUserById();
    }
  }

  parseId(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
  }

  initializeForm(): void {
    this.userForm = this.formBuilder.group({
      id: [0],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      company: ['', Validators.required],
      gender: ['', Validators.required],
      active: [true],
      contactId: [0],
      phone: ['', [Validators.required, Validators.pattern(/^\d{11}$/)]],
      city: [''],
      address: ['', Validators.required],
      country: [''],
      roleId: [0],
      roleName: ['', Validators.required]
    });
  }

  getUserById(): void {
    this.userService.getUserById(this.id).subscribe(item => this.setFormValue(item));
  }

  setFormValue(item: UserDto): void {
    this.userForm.patchValue({
      id: item.id,
      firstName: item.firstName,
      lastName: item.lastName,
      company: item.company,
      gender: item.gender,
      active: item.active,
      contactId: item.contact.id,
      phone: item.contact.phone,
      city: item.contact.city,
      address: item.contact.address,
      country: item.contact.country,
      roleId: item.role.id,
      roleName: item.role.name
    });
  }

  onSave(): void   {
    if (this.userForm.valid) {
      const user = this.getFormValueAsUserObject();
      this.isUserIdExists() ? this.updateUser(user) : this.createUser(user);
    } else {
      this.userForm.markAllAsTouched();
    }
  }

  createUser(user: UserDto): void {
    this.requestOnProgress = true;
    this.userService.createUser(user).subscribe({
      error: (error: HttpErrorResponse) => {
        this.requestOnProgress = false;
        this.showError('Failed to Create User!');
      },
      complete: () => {
        this.requestOnProgress = false;
        this.showSuccess('User Create Successfully');
      }
    });
  }

  updateUser(user: UserDto): void {
    this.requestOnProgress = true;
    this.userService.updateUser(user).subscribe({
      error: (error: HttpErrorResponse) => {
        this.requestOnProgress = false;
        this.showError('Update Failed!');
      },
      complete: () => {
        this.requestOnProgress = false;
        this.showSuccess('Update Success');
      }
    });
  }

  deleteUser(userId: number): void {
    this.requestOnProgress = true;
    this.userService.deleteUser(userId).subscribe({
      error: (error: HttpErrorResponse) => {
        this.requestOnProgress = false;
        this.showError('Delete Failed!');
      },
      complete: () => {
        this.isDeleted = true;
        this.showSuccess('Delete Success');
      }
    });
  }


  getFormValueAsUserObject(): UserDto {
    const formValue = this.userForm.value;
    const userData: UserDto = {
      id: formValue.id,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      company: formValue.company,
      gender: formValue.gender,
      active: formValue.active,
      contact: {
        id: formValue.contactId,
        phone: formValue.phone,
        city: formValue.city,
        address: formValue.address,
        country: formValue.country
      },
      role: {
        id: formValue.roleId,
        name: formValue.roleName
      }
    };
    return userData;
  }

  routeToUsersList(): void {
    this.router.navigate(['users']);
  }

  isUserIdExists = (): boolean => this.id > 0;

  showSuccess(msg: string): void {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: msg });
  }

  showError(msg: string): void {
    this.messageService.add({ severity: 'error', summary: 'Error',  detail: msg });
  }

  onToasterClose(value: ToastCloseEvent){
    if(this.isDeleted)
      this.routeToUsersList();
  }
}
