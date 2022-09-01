import { useDashboardStore, useAchievementStore, useAnswerStore, usePlayedQuestionStore, usePlayedQuizzStore, useQuestionStore, useQuizzStore, useQuizzQuestionStore, useRelationStore, useThemeStore, useUserStore, useUserRelationStore, useAuthStore } from "@/stores";

let status = false;

// MAIN function
/**
 * Checks store integrity depending on logged user.
 */
export function checkModelsStoresStatus() {
  if (!status && useAuthStore().user != null) {
    if (useAuthStore().user.userId == 1) {
      loadAllStores();
    }
    if (useAuthStore().user.userId != 1) {
      //loadAllStores();
      loadBaseStores();
      loadUserStores();
    }
    status = true;
  }
}


// MAIN support - clear and populate
/**
 * Clears all stores (doesn't clear currentModel).
 */
export function clearAllStores(){
  useAchievementStore().clearStore();
  useAnswerStore().clearStore();
  usePlayedQuestionStore().clearStore();
  usePlayedQuizzStore().clearStore();
  useQuestionStore().clearStore();
  useQuizzStore().clearStore();
  useQuizzQuestionStore().clearStore();
  useRelationStore().clearStore();
  useUserRelationStore().clearStore();
  useThemeStore().clearStore();
  useUserStore().clearStore();
  status = false;
}
/**
 * Loads all stores with everything existing in Db. Used by Admin.
 */
 function loadAllStores() {
  useAchievementStore().apiGetAll();
  useAnswerStore().apiGetAll();
  usePlayedQuestionStore().apiGetAll();
  usePlayedQuizzStore().apiGetAll();
  useQuestionStore().apiGetAll();
  useQuizzStore().apiGetAll();
  useQuizzQuestionStore().apiGetAll();
  useRelationStore().apiGetAll();
  useUserRelationStore().apiGetAll();
  useThemeStore().apiGetAll();
  useUserStore().apiGetAll();
}
/**
 * Loads the local stores with all base models needed from the Db.
 */
function loadBaseStores() {
  useThemeStore().apiGetAll();
  useRelationStore().apiGetAll();
  useQuestionStore().apiGetAllQuestionsBase();
  useAnswerStore().apiGetAllAnswersBase();

  useQuizzQuestionStore().apiGetAllQuizzQuestionsAdmin(); // -- TO FIX  --  make a filter by user
  useQuizzStore().apiGetAllQuizzesAdmin();// -- TO FIX   --  make a filter by user
}
/**
 * Adds to the local stores all models from the Db related to the logged user.
 */
function loadUserStores() {
  const loggedUserId = useAuthStore().user.userId;
 
  useAchievementStore().apiGetAllAchievementsUser(loggedUserId);
  useQuestionStore().apiGetAllQuestionsUser(loggedUserId);
  useAnswerStore().apiGetAllAnswersUser(loggedUserId);
  useQuizzStore().apiGetAllQuizzesUser(loggedUserId);
  useQuizzQuestionStore().apiGetAllQuizzQuestionsUser(loggedUserId);
  
  useUserRelationStore().apiGetAllRelationsUser(loggedUserId);
  useUserStore().apiGetAll();  // -- TO FIX -- useUserStore().apiGetAllDummyUsers(); -> receives response.data correctly, maybe doesn't convert to IUser correctly, here on the front, and store is left empty

}

