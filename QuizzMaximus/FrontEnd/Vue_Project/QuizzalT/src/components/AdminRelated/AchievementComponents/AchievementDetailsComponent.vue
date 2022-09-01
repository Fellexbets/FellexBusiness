<template>
  <div class="container">
    <h4>Achievement Details</h4>
    <table class="table">
      <thead>
        <tr>
          <th>ThemeId</th>
          <th>UserId</th>
          <th>GainedPoints</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ achievement.themeId }} </td>
          <td> {{ achievement.userId }} </td>
          <td> {{ achievement.gainedPoints}} </td>
        </tr>
      </tbody>
    </table>
    <button @click="achievementEdit(achievement)">Edit</button>
    <button @click="achievementDelete(achievement)">Delete</button>
    <button @click="back()">Back</button>
  </div>
  
</template>

<script lang="ts">
import { defineComponent} from "vue";
import { mapStores } from "pinia";
import { useAchievementStore } from "@/stores/";
import type { IAchievement } from "@/models/";

export default defineComponent({
  data() {
    return {
      achievement: {} as IAchievement
    }
  },
  methods: {
    achievementEdit(achievement : IAchievement){
      this.achievementStore.setCurrentAchievement(achievement);
      this.$emit('changePage', 'AchievementEditComponent');
    },
    achievementDelete(achievement: IAchievement){
      this.achievementStore.setCurrentAchievement(achievement);
      this.$emit('changePage', 'AchievementDeleteComponent');
    },
    back() {
      this.$emit('changePage', 'AchievementIndexComponent');
    },

  },
  mounted() {
   this.achievement = this.achievementStore.getCurrentAchievement;
  },
  computed: {
    ...mapStores(useAchievementStore)
  }
})
</script>