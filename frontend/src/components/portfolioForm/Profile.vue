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
        ></v-text-field>
      </v-flex>

      <v-flex xs6 px-2>
        <v-text-field
          v-model="profile.nameEng"
          label="氏名（英字）"
          required
        ></v-text-field>
      </v-flex>

      <!-- <v-flex xs12 px-2>
        <v-menu
          v-model="datepicker"
          :close-on-content-click="false"
          :nudge-right="40"
          lazy
          transition="scale-transition"
          offset-y
          full-width
          min-width="290px"
        >
          <v-text-field
            slot="activator"
            v-model="profile.birthday"
            label="生年月日"
            prepend-icon="event"
            readonly
          ></v-text-field>
          <v-date-picker v-model="profile.birthday" @input="datepicker = false"></v-date-picker>
        </v-menu>
      </v-flex> -->

      <v-flex xs12 px-2>
          <v-textarea
            v-model="profile.detail"
            label="自己PR"
          ></v-textarea>
          
          <!-- <v-btn
            :disabled="!valid"
            color="success"
            @click="validate"
          >
            Validate
          </v-btn>

          <v-btn
            color="error"
            @click="reset"
          >
            Reset Form
          </v-btn>

          <v-btn
            color="warning"
            @click="resetValidation"
          >
            Reset Validation
          </v-btn> -->     
      </v-flex>
    </v-layout>
  </v-form>
  <!-- <v-divider light></v-divider> -->
</template>

<script>
export default {
  data: () => ({
    visible:  false,
    valid: true,
    datepicker: false,
    profile: {
      name: '',
      nameEng: '',
      photoSrc: null,
      birthday: '',
      detail: ''
    }
  }),
  props: {
    account: Object
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
    pickPhoto () {
      this.$refs.image.click();
    },
		onPhotoPicked (e) {
			const files = e.target.files;
      const fr = new FileReader ()
      fr.readAsDataURL(files[0])
      fr.addEventListener('load', () => {
        this.profile.photoSrc = fr.result
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
</style>
