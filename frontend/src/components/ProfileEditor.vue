<template>
  <v-form
    ref="form"
    v-model="valid"
    lazy-validation
  >
    <v-layout row wrap>
      <v-flex offset-xs3 xs6 pb-2>
        <div class="profile-container">
          <img class="profile-photo" :src="photoSrc" />
          <v-btn large @click="pickPhoto()" flat icon outline color="accent" class="profile-photo-button">
            <v-icon>camera_alt</v-icon>
          </v-btn>
          <input type="file" style="display: none" ref="image" accept="image/*" @change="onPhotoPicked">
        </div>
      </v-flex>
    </v-layout>

    <v-layout row wrap>
      <v-flex xs6 px-2>
        <v-text-field
          v-model="profile.name"
          label="氏名（漢字）"
          required
          @change="onChangeForm"
        ></v-text-field>
      </v-flex>

      <v-flex xs6 px-2>
        <v-text-field
          v-model="profile.nameEng"
          label="氏名（英字）"
          required
          @change="onChangeForm"
        ></v-text-field>
      </v-flex>
    </v-layout>

    <v-layout row wrap>
      <v-flex xs12 px-2>
          <v-textarea
            v-model="profile.detail"
            label="自己PR"
            @change="onChangeForm"
          ></v-textarea>
      </v-flex>
    </v-layout>

    <v-layout row wrap d-inline-flex align-content-center>
      <v-flex xs12 d-flex>

        <template v-if="saving">
          <v-progress-circular
            indeterminate
            color="accent"
            size="20"
          ></v-progress-circular>
          <strong class="profile-save">Saving...</strong>
        </template>

        <template v-if="saved">
          <v-icon color="accent">check_circle</v-icon>
          <strong class="profile-save">Saved!</strong>
        </template>

      </v-flex>
    </v-layout>
  </v-form>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  data: function () {
    return {
      visible: false,
      valid: true,
      datepicker: false,
      profile: this.account,
      timer: {},
      saving: false,
      saved: false
    }
  },
  props: {
    account: {
      type: Object,
      default: function () {
        return {
          name: '',
          nameEng: '',
          photoSrc: '',
          birthday: '',
          detail: ''
        }
      }
    }
  },
  computed: {
    photoSrc: function () {
      if (!this.profile.photoSrc) {
        return require('@/assets/baseline-account_box-24px.png')
      }
      return this.profile.photoSrc
    }
  },
  methods: {
    ...mapActions({
      edit: 'auth/edit'
    }),
    pickPhoto () {
      this.$refs.image.click()
    },
    onPhotoPicked (e) {
      const files = e.target.files
      const fr = new FileReader()
      fr.readAsDataURL(files[0])
      fr.addEventListener('load', () => {
        this.profile.photoSrc = fr.result
        this.onChangeForm()
      })
    },
    onChangeForm () {
      this.saving = true
      this.edit(this.profile)
        .then(() => {
          this.saved = true
          setTimeout(() =>{
            this.saved = false  
          }, 1000)
        })
        .finally(() => {
          this.saving = false
        })
    }
  }
}
</script>

<style scoped>
.profile-container{
  position: relative;
  display: flex;
  justify-content: center;
}
.profile-photo{
  height: 30vh;
  border-radius: 20%;
}
.profile-photo-button{
  background: #FFFFFF !important;
  position:absolute;
  z-index: 1;
  left: 75%;
  bottom: 0%;
}
.profile-save{
  margin-left: 4px;
  padding-top: 2px;
  color:#65BAC4;
}
</style>
