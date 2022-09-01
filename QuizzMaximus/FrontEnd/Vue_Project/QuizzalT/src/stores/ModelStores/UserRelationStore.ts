import { defineStore } from 'pinia'
import type { IUser, IUserRelation } from '@/models/'
import { UserRelationApi } from "@/api/";
import { useUserStore } from './UserStore';

export const useUserRelationStore = defineStore({
  id: 'userRelation',
  state: () => ({
    currentUserRelation: {} as IUserRelation,
    userRelations: [] as IUserRelation[]
  }),
  getters: {
    getCurrentUserRelation: (state) => state.currentUserRelation,
    getUserRelations: (state) => state.userRelations,

  },
  actions: {
    // Store methods - gets and sets
    setCurrentUserRelation(userRelation: IUserRelation): void {
      this.currentUserRelation = userRelation;
    },
    setUserRelations(userRelations: IUserRelation[]): void {
      this.userRelations = userRelations
    },
    getFriendName(relatedUserId: number): string{
      const friendName = useUserStore().getUsers.find((u) => u.userId == relatedUserId )
      return friendName?.username as string;
    },
    /**
     * Gets all ids of related users of the user.
     * @param userId UserId to filter.
     * @returns All UserRelations Ids of the user.
     */
    getAllRelatedUsersIds(userId: number): number[] {
      let relatedUsersIds = [] as number[];
      let userRelations = this.userRelations.filter(relation => relation.userId == userId);
      userRelations.forEach(uR => relatedUsersIds.push(uR.relatedUserId));
      return relatedUsersIds;
    },
    /**
     * Gets all UserRelations of the user.
     * @param userId UserId to filter.
     * @returns All UserRelations of the user.
     */
    getAllUserRelations(userId: number): IUserRelation[] {
      return this.userRelations.filter(relation => relation.userId == userId)
    },
    /**
     * Gets all UserRelations of the user filtered by the type of relation (relationId).
     * @param userId UserId to filter.
     * @param relationId RelationId to filter.
     * @returns All UserRelations of the user filtered by the type of relation.
     */
    getFilteredUserRelations(userId: number, relationId: number) {
      return this.userRelations.filter(relation => relation.relationId == relationId && relation.userId == userId)
    },
    // Store methods - crud methods (may break sync with DB) | create/update -> set the current quizz to the created/updated one
    addUserRelation(userRelation: IUserRelation): void {
      this.userRelations.push(userRelation);
      this.currentUserRelation = userRelation;
    },
    updateUserRelation(userRelation: IUserRelation): void {
      const index = this.userRelations.findIndex((u) => u.userId == userRelation.userId && u.relatedUserId == userRelation.relatedUserId);
      this.userRelations.splice(index, 1, userRelation);
      this.currentUserRelation = userRelation;
    },
    deleteUserRelation(userId: number, relatedUserId: number): void {
      const index = this.userRelations.findIndex((u) => u.userId == userId && u.relatedUserId == relatedUserId);
      this.userRelations.splice(index, 1);
    },
    clearStore() {
      this.userRelations = [];
    },
    // API methods - sync with store on success only| Error: api fail / Warning: not accepted | create/update -> set the current quizz to the created/updated one (due to "Store methods")
    apiGetAll(): IUserRelation[] {
      UserRelationApi.getAll()
        .then((response) => {
          this.userRelations = response.data;
          return this.userRelations;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserRelationStore - apiGetAll() | ", error);
        })
      return this.userRelations;
    },
    apiGetById(userId: number, relatedUserId: number): IUserRelation {
      UserRelationApi.getById(userId, relatedUserId)
        .then((response) => {
          this.currentUserRelation = response.data;
          return this.currentUserRelation;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserRelationStore - apiGetById(userRelationId) | ", error);
        })
        return this.currentUserRelation;
    },
    apiCreate(userRelation: IUserRelation): boolean {
      UserRelationApi.create(userRelation)
        .then((response) => {
          if (response.status == 200) {
            this.addUserRelation(response.data);
            console.log('%c Success! UserRelation created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: UserRelationStore - apiCreate(userRelation)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserRelationStore - apiCreate(userRelation) | ", error);
          return false;
        })
        return true;
    },
    apiUpdate(userRelation: IUserRelation): boolean {
      UserRelationApi.update(userRelation)
        .then((response) => {
          if (response.data == true) {
            this.updateUserRelation(userRelation);
            console.log('%c Success! UserRelation updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: UserRelationStore - apiUpdate(userRelation)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserRelationStore - apiUpdate(userRelation) | ", error);
          return false;
        })
        return true;
    },
    apiDelete(userId: number, relatedUserId: number): boolean {
      UserRelationApi.delete(userId, relatedUserId)
        .then((response) => {
          if (response.data == true) {
            this.deleteUserRelation(userId, relatedUserId);
            console.log('%c Success! UserRelation deleted from Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: UserRelationStore - apiDelete(userRelationId)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: UserRelationStore - apiDelete(userRelationId) | ", error);
          return false;
        })
        return true;
    },
    apiGetAllRelationsUser(userId: number) : IUserRelation[]{
      UserRelationApi.getAllRelationsUser(userId)
      .then((response) => {
        this.userRelations = response.data;
        return response.data;
      })
      .catch((error) => {
        console.error("Error: API connection | Location: apiGetAllRelationsUser(userId) | ", error);
      })
    return this.userRelations;
    }
  }
})
