<template>
  <NavBarComponent></NavBarComponent>
  <div id="signUpPage">
    <div class="container w-50 p-3 boxShadow" id="signUpDiv">
      <h1 class="text-center">Sign up</h1>

      <form @submit.prevent="signup()">

        <label for="inputSignupUsername" class="form-label">Username</label>
        <input v-model="user.username" type="username" class="form-control" id="inputSignupUsername">

        <label for="inputSignupEmail" class="form-label">Email</label>
        <input v-model="user.email" type="email" class="form-control" id="inputSignupEmail">

        <label for="inputSignupPassword" class="form-label">Password</label>
        <input v-model="user.password" type="password" class="form-control" id="inputSignupPassword">
        <br />

        <div>
          <div v-if="!previewImage.length">
            <label Htmlfor="inputPhoto">Photo (optional)</label>
            <input type="file" @change="receiveImage" accept="image/x-png,image/gif,image/jpeg" class="form-control" id="inputPhoto">
          </div>

          <div v-else>
            <p class="textCenter">Photo</p>
            <img :src="`data:image/png;base64,${previewImage}`" alt="photo" class="mx-auto d-block" id="signUpPhoto"/> <br />
            <button @click.prevent="removeImage()" class="mx-auto d-block">Remove Image</button>
          </div>
        </div>
        <br />

        <button class="w-100" type="submit">Sign Up</button><br /><br />
        <button class="w-100" @click="$router.push(`/login`)">Login page</button>
        <br /><br />
      </form>

    </div>
    <br /><br /><br /><br />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useUserStore } from "@/stores/";
import type { IUser } from "@/models/";

export default defineComponent({
  data() {
    return {
      user: {} as IUser,
      errorinfo: '',
      previewImage: {} as string
    }
  },
  methods: {
    signup() {
      this.user.role = "Player";
      this.userStore.apiCreate(this.user);
      this.$router.push(`/login`);
    },
    async receiveImage(e: any) {
      if (e != null) {
        const selectedImage = e.target.files[0];
        this.user.photoString = await this.userStore.createBase64Image(selectedImage);
        this.previewImage = this.user.photoString;
      }
    },
    removeImage() {
      this.user.photoString = '';
      this.previewImage = '';
    },
    back() {
      this.$router.push('/');
    }
  },
  computed: {
    ...mapStores(useUserStore)
  }
})
</script>