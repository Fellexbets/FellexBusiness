<template>
    <div class="popupUserPhoto">
        <div class="popupInnerUserPhoto">
            <form @submit.prevent="changePhoto()">
              <div>
                <div v-if="!previewImage.length">
                  <label Htmlfor="inputPhoto">Photo</label>
                  <input type="file" @change="receiveImage" accept="image/x-png,image/gif,image/jpeg" class="form-control" id="inputPhoto">
                </div>

                <div v-else>
                  <p class="textCenter">Photo</p>
                  <img :src="`data:image/png;base64,${previewImage}`" alt="photo" class="mx-auto d-block" id="signUpPhoto"/> <br />
                  <button @click.prevent="removeImage()" class="updatePhotoButtons">Remove Image</button>
                </div>
                <button type="submit" class="updatePhotoButtons">Submit</button>
                <button @click.prevent="cancel()" class="updatePhotoButtons">Cancel</button>
              </div>
            </form>
        </div>
    </div>
</template>

<script lang="ts">
import type { IUser } from "@/models";
import { useAuthStore, useUserStore } from "@/stores";
import { mapStores } from "pinia";
import { defineComponent } from "vue";

 export default defineComponent({
    data(){
        return{
            user: {} as IUser,
            previewImage: {} as string | any[]
        }
    },
    methods: {
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
        changePhoto(){
            this.userStore.updateUserP(this.user);
            //console.log(this.user.userId, this.user.photoString)
            this.$emit("closePopUp");
        },
        cancel(){
            this.$emit("closePopUp");
        }
    },
    mounted(){
        this.user = this.authStore.user;
        if(this.authStore.user.photo){
            this.previewImage = this.getPhoto;
        }
    },
    computed: {
        ...mapStores(useAuthStore, useUserStore),
        getPhoto: function() : string{
            return this.authStore.user.photo;
        }
    },
    emits: ["closePopUp"]
 })
</script>