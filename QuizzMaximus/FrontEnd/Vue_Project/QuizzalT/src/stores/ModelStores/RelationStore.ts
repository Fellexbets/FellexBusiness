import { defineStore } from 'pinia'
import type { IRelation } from '@/models/'
import { RelationApi } from "@/api/";

export const useRelationStore = defineStore({
  id: 'relation',
  state: () => ({
    currentRelation: {} as IRelation,
    relations: [] as IRelation[]
  }),
  getters: {
    getCurrentRelation: (state) => state.currentRelation,
    getRelations: (state) => state.relations
  },
  actions: {
    // Store methods - gets and sets
    setCurrentRelation(relation: IRelation): void {
      this.currentRelation = relation;
    },
    setRelations(relations: IRelation[]): void {
      this.relations = relations
    },
    getRelationName(relationId: number): string {
      return this.relations.find(t => t.relationId == relationId)?.relationName as string;
    },
    // Store methods - crud methods (may break sync with DB) | create/update -> set the current quizz to the created/updated one
    addRelation(relation: IRelation): void {
      this.relations.push(relation);
      this.currentRelation = relation;
    },
    updateRelation(relation: IRelation): void {
      const index = this.relations.findIndex((u) => u.relationId == relation.relationId);
      this.relations.splice(index, 1, relation);
      this.currentRelation = relation;
    },
    deleteRelation(relationId: number): void {
      const index = this.relations.findIndex((u) => u.relationId == relationId);
      this.relations.splice(index, 1);
    },
    clearStore() {
      this.relations = [];
    },
    // API methods - sync with store on success only| Error: api fail / Warning: not accepted | create/update -> set the current quizz to the created/updated one (due to "Store methods")
    apiGetAll(): IRelation[] {
      RelationApi.getAll()
        .then((response) => {
          this.relations = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: RelationStore - apiGetAll() | ", error);
        })
      return this.relations;
    },
    apiGetById(relationId: number): IRelation {
      RelationApi.getById(relationId)
        .then((response) => {
          this.currentRelation = response.data;
        })
        .catch((error) => {
          console.error("Error: API connection | Location: RelationStore - apiGetById(relationId) | ", error);
        })
        return this.currentRelation;
    },
    apiCreate(relation: IRelation): boolean {
      RelationApi.create(relation)
        .then((response) => {
          if (response.status == 200) {
            this.addRelation(response.data);
            console.log('%c Success! Relation created and added to store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: RelationStore - apiCreate(relation)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: RelationStore - apiCreate(relation) | ", error);
          return false;
        })
        return true;
    },
    apiUpdate(relation: IRelation): boolean {
      RelationApi.update(relation)
        .then((response) => {
          if (response.data == true) {
            this.updateRelation(relation);
            console.log('%c Success! Relation updated in Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: RelationStore - apiUpdate(relation)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: RelationStore - apiUpdate(relation) | ", error);
          return false;
        })
        return true;
    },
    apiDelete(relationId: number): boolean {
      RelationApi.delete(relationId)
        .then((response) => {
          if (response.data == true) {
            this.deleteRelation(relationId);
            console.log('%c Success! Relation deleted from Db and store.', 'color: green;');
            return true;
          }
          else {
            console.warn("Warning: not accepted by API | Location: RelationStore - apiDelete(relationId)");
            return false;
          }
        })
        .catch((error) => {
          console.error("Error: API connection | Location: RelationStore - apiDelete(relationId) | ", error);
          return false;
        })
        return true;
    }

  }
})
