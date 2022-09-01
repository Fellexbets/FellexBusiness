import { createRouter, createWebHistory } from 'vue-router'
import { checkModelsStoresStatus } from '@/logic/ApiReloader'
import { useAuthStore } from '@/stores'
import { AchievementView, AnswerView, PlayedQuizzView, PlayedQuestionView , QuestionView, QuizzView,
QuizzQuestionView, RelationView, ThemeView, UserView, UserRelationView} from '@/views/AdminWizard/'

//General area
import HomeView from '@/views/HomeView.vue'
import SignupView from '@/views/SignupView.vue'
import ProfileView from '@/views/ProfileView.vue'
import LoginView from '@/views/LoginView.vue'

//User area
import MyQuizzesView from '@/views/UserViews/MyQuizzesView.vue'
import MyQuestionsView from '@/views/UserViews/MyQuestionsView.vue'

//Misc area
import QuizzalTGameView from '@/views/QuizzalTGameView.vue'
import DashboardView from '@/views/DashboardView.vue'

export const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  linkActiveClass: 'active',
  routes: [
    { path: '/', name: 'home', component: HomeView },

    { path: '/achievement',    name: 'achievement',     component: AchievementView },
    { path: '/answer',         name: 'answer',          component: AnswerView },
    { path: '/playedQuestion', name: 'playedQuestion',  component: PlayedQuestionView },
    { path: '/playedQuizz',    name: 'playedQuizz',     component: PlayedQuizzView },
    { path: '/question',       name: 'question',        component: QuestionView },
    { path: '/quizz',          name: 'quizz',           component: QuizzView },
    { path: '/quizzQuestion',  name: 'quizzQuestion',   component: QuizzQuestionView },
    { path: '/relation',       name: 'relation',        component: RelationView },
    { path: '/theme',          name: 'theme',           component: ThemeView },
    { path: '/user',           name: 'user',            component: UserView },
    { path: '/userRelation',   name: 'userRelation',    component: UserRelationView },

    { path: '/login',   name: 'login',   component: LoginView },
    { path: '/signup',  name: 'signup',  component: SignupView },
    { path: '/profile', name: 'profile', component: ProfileView },

    { path: '/myQuizzes',    name: 'myQuizzes',   component: MyQuizzesView },
    { path: '/myQuestions',  name: 'myQuestions', component: MyQuestionsView },

    { path: '/gamealt',  name: 'gamealt',  component: QuizzalTGameView },
    { path: '/dashboard', name: 'dashboard', component: DashboardView }
  ]
})

export default router
  

router.beforeEach( (to) => {
  checkModelsStoresStatus();
 
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/login', '/', '/profile', '/signup', '/myQuizzes', '/myQuestions', '/gamealt'];
  const adminPages = ['/dashboard', '/userIndex', '/userDelete', '/answerIndex', '/achievementIndex', '/playedQuestionIndex', '/questionIndex', '/quizzIndex', '/themeIndex', '/relationIndex']
  const authRequired = !publicPages.includes(to.path);
  const adminRequired = adminPages.includes(to.path);
  const auth = useAuthStore();

  if (authRequired && auth.user?.role != "Admin" && auth.user?.role != "Player") {
      //auth.returnUrl = to.fullPath;
      router.push('/');
  }
  if (adminRequired && auth.user?.role == "Player") {
    //auth.returnUrl = to.fullPath;
    router.push('/profile');
}

});


