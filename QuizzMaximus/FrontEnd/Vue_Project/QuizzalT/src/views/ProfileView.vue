<template>

  <NavBarComponent></NavBarComponent>

  <div class="container" v-if="authStore.user" id="profileContainer"><!-- Player Info -->
    <h3 class="text-center">Profile</h3><br />
      <div v-if="authStore.user.photo" class="profilePhotoDiv">
        <img :src="`data:image/png;base64,${authStore.user.photo}`" alt="photo" id="profilePhoto"/>
        <br/>
      </div>
    <UpdatePhoto v-if="updatePhotoPopUp" @close-pop-up="closeChangePhoto()"></UpdatePhoto>

    <div class="changePhotoDiv">
    <button @click="changePhoto()" class="updatePhotoButtons">Change Photo</button>
    </div>
    
    <table class="table">
      <thead>
        <th>Username</th>
        <th>Email</th>
        <th>Date Registered</th>
      </thead>
      <tbody>
        <td>{{ authStore.user.username }}</td>
        <td>{{ authStore.user.email }}</td>
        <td>{{ authStore.user.dateCreated }}</td>
      </tbody>
    </table>
    <br /><br />

    <div class="container trofeu" v-if="achievementStore.getGoldTrophies() != 0">
        <h3 >Your golden trophies! </h3>
        <img  src="src/assets/trophies/trophy.png">
        <h3 >{{achievementStore.getGoldTrophies()}} </h3>
    </div>
    <br /><br />

    <div class="container"><!-- My Quizzes -->
      <h5 class="text-center">My Quizzes</h5>
      <div class="profileButtonsDiv">
        <button @click="myQuizzes()" class="myAreaButtons">My Quizzes</button>
        <button @click="myQuestions()" class="myAreaButtons">My Questions</button>
      </div>
    </div>
    <br /><br />

    <div class="container" v-if="authStore.getUserAchievements.length != 0"> <!-- My Achievements -->
      <details>
        <summary>
          <h5 style="display: inline;">My Achievements</h5>
        </summary>
        <div class="scrollable table">
          <table class="w-100">
            <thead>
              <th>Theme</th>
              <th>Gained Points</th>
              <th> Trophies </th>
            </thead>
            <tbody>
              <tr v-for="item in authStore.getUserAchievements">
                <td>{{ themeStore.getThemeName(item.themeId) }}</td>
                <td>{{ item.gainedPoints }}</td>
                <img v-if="item.gainedPoints >= 500" src="src/assets/trophies/gold.png">
                <img v-if="item.gainedPoints >= 200 && item.gainedPoints < 500" src="src/assets/trophies/silver.png">
                <img v-if="item.gainedPoints < 200" src="src/assets/trophies/bronze.png">
              </tr>

            </tbody>
          </table>
        </div>
      </details>
    </div>
    <br />

    <div class="container" v-if="authStore.getUserRelations.length != 0"><!-- My Relations -->
    <details>
      <summary>
        <h5 style="display: inline;">My Relations</h5>
      </summary>

      <div v-if="authStore.getUserRelBffs.length != 0">
        <details>
          <summary>
            <h6 style="display: inline;">My Bffs</h6>
          </summary>
          <div class="scrollable table">
            <table class="w-100">
              <thead>
                <th>User</th>
                <th>User Id</th>
                <th>Date Created</th>
              </thead>
              <tbody>
                <tr v-for="item in authStore.getUserRelBffs">
                  <td>{{ userStore.getUserNameById(item.relatedUserId) }}</td>
                  <td>{{ item.relatedUserId }}</td>
                  <td>{{ item.dateCreated }}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </details>
      </div>

      <div v-if="authStore.getUserRelFriends.length != 0">
        <details>
          <summary>
            <h6 style="display: inline;">My Friends</h6>
          </summary>
          <div class="scrollable table">
            <table class="w-100">
              <thead>
                <th>User</th>
                <th>User Id</th>
                <th>Date Created</th>
              </thead>
              <tbody>
                <tr v-for="item in authStore.getUserRelFriends">
                  <td>{{ userStore.getUserNameById(item.relatedUserId) }}</td>
                  <td>{{ item.relatedUserId }}</td>
                  <td>{{ item.dateCreated }}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </details>
      </div>

      <div v-if="authStore.getUserRelWatch.length != 0">
        <details>
          <summary>
            <h6 style="display: inline;">My WatchList</h6>
          </summary>
          <div class="scrollable table">
            <table class="w-100">
              <thead>
                <th>User</th>
                <th>User Id</th>
                <th>Date Created</th>
              </thead>
              <tbody>
                <tr v-for="item in authStore.getUserRelWatch">
                  <td>{{ userStore.getUserNameById(item.relatedUserId) }}</td>
                  <td>{{ item.relatedUserId }}</td>
                  <td>{{ item.dateCreated }}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </details>
      </div>

      <div v-if="authStore.getUserRelBlocked.length != 0">
        <details>
          <summary>
            <h6 style="display: inline;">Blocked Users</h6>
          </summary>
          <div class="scrollable table">
            <table class="w-100">
              <thead>
                <th>User</th>
                <th>User Id</th>
                <th>Date Created</th>
              </thead>
              <tbody>
                <tr v-for="item in authStore.getUserRelBlocked">
                  <td>{{ userStore.getUserNameById(item.relatedUserId) }}</td>
                  <td>{{ item.relatedUserId }}</td>
                  <td>{{ item.dateCreated }}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </details>
      </div>
      </details>
    </div>
    <br />

    <CrudMenu v-if="authStore.checkIfAdmin"></CrudMenu>
    <br /><br /><br />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useAuthStore, useAchievementStore, useQuestionStore, useUserStore, useUserRelationStore, useThemeStore } from "@/stores";
import { CrudMenu } from "@/components/";
import UpdatePhoto from "@/components/MyAreaComponents/UpdatePhoto.vue"

export default defineComponent({
  data(){
    return{
      updatePhotoPopUp: false
    }
  },
  mounted(){
    console.log(this.authStore.user);
  },
  methods: {
    myQuizzes() {
      this.$router.push(`/myQuizzes`);
    },
    myQuestions() {
      this.$router.push(`/myQuestions`);
    },
    changePhoto(){
      this.updatePhotoPopUp = true;
    },
    closeChangePhoto(){
      this.updatePhotoPopUp = false;
    }
  },
  computed: {
    ...mapStores(useAuthStore, useUserStore, useAchievementStore, useUserRelationStore, useQuestionStore, useThemeStore)
  },
  components: {
    CrudMenu,
    UpdatePhoto
  }
})
</script>

