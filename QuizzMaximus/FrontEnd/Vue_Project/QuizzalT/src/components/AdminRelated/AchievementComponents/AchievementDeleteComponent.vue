<template>
  <div class="container">
    <h4>Delete Achievement</h4>
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
          <td> {{ achievement.gainedPoints }} </td>
        </tr>
      </tbody>
    </table>
    <p> Are you sure?</p>
    <button @click="achievementDelete(achievement.themeId, achievement.userId)">Delete</button>
    <button @click="back()">Back</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
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
    achievementDelete(themeId: number, userId: number) {
      this.achievementStore.apiDelete(themeId, userId);
      this.$emit('changePage', 'AchievementIndexComponent')
    },
    back() {
      this.$emit('changePage', 'AchievementIndexComponent')
    }
  },
  mounted() {
    this.achievement = this.achievementStore.getCurrentAchievement;
  },
  computed: {
    ...mapStores(useAchievementStore)
  },
  components: {
  }
})
</script>