import { User } from '../../shared/users/user';
export class Comments {
  id?: number;
  memeId: number;
  content: string;
  dateAdded?: string;
  author?: User;
  length: number;
}
