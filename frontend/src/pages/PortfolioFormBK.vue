<template>
  <layout-baseline>
    <v-container>
      <v-form
          ref="form"
          v-model="valid"
          lazy-validation
        >
          <v-text-field
            v-model="profile.userName"
            label="氏名（漢字）"
            required
          ></v-text-field>

          <v-text-field
            v-model="profile.userNameEng"
            label="氏名（英字）"
            required
          ></v-text-field>

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

          <v-textarea
            name="input-7-4"
            label="自由記入欄"
            value="The Woodman set to work at once, and so sharp was his axe that the tree was soon chopped nearly through."
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
        </v-form>
    </v-container>
  </layout-baseline>
</template>

<script>
import { mapActions, mapState } from 'vuex'

export default {
  data: () => ({
    visible:  false,
    valid: true,
    datepicker: false,
    profile: {
      userName: '栗原　萌実',
      userNameEng: 'Megumi Kurihara',
      birthday: ''
    }
  })
}
</script>
