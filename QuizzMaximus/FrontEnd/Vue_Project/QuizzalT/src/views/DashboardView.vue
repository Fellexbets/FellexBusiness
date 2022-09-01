<template>
  <NavBarComponent></NavBarComponent>

  <div class="container" v-if="dashboardStore.getUsersListed != undefined">
    <h2>Dashboard</h2> <br />
   
    <div>
      <h5>Highscores</h5> <br />
    </div>

    <div class="autogrid">
      <div class="grid_row autogrid grid_Table_Headers">
        <div class="gridCell centerText" v-for="item in dashboardStore.getThemesNotEmpty">{{ item.themeName }}</div>
      </div>
      <div class="grid_row autogrid">
        <div class="gridCell centerText" v-for="item in dashboardStore.getThemesNotEmpty">{{ dashboardStore.getUsersListed.getThemeTopUserName(item.themeId)}}</div>
      </div>
      <div class="grid_row autogrid">
        <div class="gridCell centerText" v-for="item in dashboardStore.getThemesNotEmpty">
          <!--<img class="gridCell dashboard_userImg" :src="dashboardStore.getUsersListed.getThemeTopUserSrc(item.themeId)" alt="Photo" />-->
          <img class="gridCell dashboard_userImg" :src="userStore.getUserPhotoSrc(dashboardStore.getUsersListed.getThemeTopUser(item.themeId).userId)" alt="Photo" />
        </div>
      </div>
      <div class="grid_row autogrid">
        <div class="gridCell centerText" v-for="item in dashboardStore.getThemesNotEmpty">{{ dashboardStore.getUsersListed.getThemeTopUserPoints(item.themeId)}}</div>
      </div>
    </div>
    <br /><hr /><br />

    <div>
      <h5>Scores by theme</h5>
    </div>

    <div v-for="theme in dashboardStore.getThemesNotEmpty">
      <br /> <h6><strong>{{ theme.themeName }}</strong></h6>

      <div class="autogrid grid9" v-for="user in dashboardStore.getUsersListed.getUsersWithAchievOfThemeSorted(theme.themeId)">
        <div class="gridCell grid9_Table3Dash_columnLeft rightAlign">{{ user.username }}</div>
        <div class="gridCell grid9_Table3Dash_columnMiddle leftAlign">
          <img class="dashboard_userImg" :src="userStore.getUserPhotoSrc(user.userId)" alt="Photo" />
        </div>
        <div class="gridCell grid9_Table3Dash_columnRight leftAlign">
          <div class="pointsBar" :style="{ width: dashboardStore.getUsersListed.getUserPointsOfThemeInPercentage(user.userId, theme.themeId) + '%' }">
            {{ dashboardStore.getUsersListed.getUserPointsOfTheme(user.userId, theme.themeId)}}
          </div>
        </div>
      </div>
    </div>
    <br /> <br /> <br />

  </div>

  <div v-else>
    Something went wrong. Please contact our customer support.
  </div>

</template>


<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useDashboardStore, useThemeStore, useUserStore } from "@/stores/";
import type { IUser } from "@/models";

export default defineComponent({
  computed: {
    ...mapStores(useDashboardStore, useUserStore, useThemeStore),
  },
  beforeMount() {
    //this.dashboardStore.startStore(this.userStore.getUsers);
  },

})
</script>
