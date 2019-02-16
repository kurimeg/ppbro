<template>
  <layout-baseline
    :headerName="this.headerName"
    :isUser="this.isUser"
  >
    <v-container>
      <v-layout row>
        <v-flex offset-xs11 xs1>
          <v-btn color="accent" @click="onIssue">発行</v-btn>
        </v-flex>
      </v-layout>
      <v-layout row>
        <v-flex xs12>
          <v-data-table
              v-model="selected"
              :headers="headers"
              :items="certificates"
              item-key="issuerName"
              select-all
              class="elevation-1"
            >
              <template slot="items" slot-scope="props">
                <td style="width: 5vw;">
                  <v-checkbox
                    v-model="props.selected"
                    primary
                    hide-details
                  ></v-checkbox>
                </td>
                <td>{{ props.item.issuerName }}</td>
              </template>
            </v-data-table>
        </v-flex>
      </v-layout>
    </v-container>
  </layout-baseline>
</template>

<script>
import { mapActions, mapState } from 'vuex'
import TimelineCertificate from '@/components/TimelineCertificate'
import Profile from '@/components/Profile'

export default {
  components: {
    TimelineCertificate,
    Profile
  },
  data: function () {
    return {
      headerName: '発行',
      isUser: false,
      pagination: {
        sortBy: 'userName'
      },
      selected: [],
      headers: [
        {
          text: '要求者名',
          align: 'left',
          value: 'issuerName'
        }
      ],
    }
  },
  computed: {
    ...mapState('certificate', ['certificates'])
  },
  created: function () {
    this.initialize()
  },
  methods: {
    ...mapActions({
      fetchRequests: 'certificate/fetchRequests'
    }),
    initialize: function () {
      this.fetchRequests()
    },
    toggleAll () {
        if (this.selected.length) this.selected = []
        else this.selected = this.requests.slice()
    },
    changeSort (column) {
      if (this.pagination.sortBy === column) {
        this.pagination.descending = !this.pagination.descending
      } else {
        this.pagination.sortBy = column
        this.pagination.descending = false
      }
    },
    onIssue () {

    }
  }
}
</script>
