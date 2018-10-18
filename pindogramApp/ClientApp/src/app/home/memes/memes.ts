import { User } from '../../shared/users/user';
export class Memes {
  id?: number;
  title: string;
  image: string;
  likes?: number;
  dateAdded?: string;
  canUpvote?: boolean;
  canDownvote?: boolean;
  author?: User;
}
