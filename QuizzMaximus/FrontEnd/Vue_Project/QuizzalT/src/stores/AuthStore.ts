import { defineStore } from 'pinia'
import { router } from '@/router';
import { UserApi } from "@/api/";
import type { IUser } from '@/models';
import { clearAllStores } from '@/logic/ApiReloader';
import { useAchievementStore, useUserRelationStore } from '@/stores/';

/**
 * Authorization/Authentication store. Stores logged user and auth methods.
 */
export const useAuthStore = defineStore({
  id: 'auth',
  state: () => ({
    user: JSON.parse(localStorage.getItem('user') as string),  //{ userId: 1, username: "MyAdmin", email: "Admin@email.com", role:"Admin", password:"123qwerty" } as IUser,
  }),
  getters: {
    checkIfAdmin:        (state) => state.user.userId == 1,
    
    getUserAchievements: (state) => useAchievementStore().getAchievementsOfUser(state.user.userId),
    getUserRelations:    (state) => useUserRelationStore().getAllUserRelations(state.user.userId),
    getUserRelationsIds: (state) => useUserRelationStore().getAllRelatedUsersIds(state.user.userId),
    getUserRelBffs:      (state) => useUserRelationStore().getFilteredUserRelations(state.user.userId, 1),
    getUserRelFriends:   (state) => useUserRelationStore().getFilteredUserRelations(state.user.userId, 2),
    getUserRelWatch:     (state) => useUserRelationStore().getFilteredUserRelations(state.user.userId, 3),
    getUserRelBlocked:   (state) => useUserRelationStore().getFilteredUserRelations(state.user.userId, 4),
  },
  actions: {
    /**
     * Login function. Stores the logged user in the local storage if successful.
     * @param user The user making the "login".
     */
    authenticate(user: IUser): void {
      UserApi.authenticate(user)
      .then((response) => {
        if(response.status == 200) {
          this.user = response.data;
          localStorage.setItem('user', JSON.stringify(this.user)); 
          router.push('/profile')
          console.log("User logged.", response)
        }
        else {
          console.log("Warning: User NOT logged.", response)
        }
      })
      .catch((error) => {
        console.log("Error: User NOT logged.", error); 
      })
    },
    /**
     * Logout function. Clears the logged user from the local storage and store.
     */
    logout(): void  {
      this.user = null;
      clearAllStores();
      localStorage.removeItem('user');
      router.push('/login');
      console.log("User logged out.");
    },
    updatePhoto(user: IUser){
      this.user.photo = user.photo;
    }
  }
})


